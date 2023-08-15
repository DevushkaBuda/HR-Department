using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace HumanResourcesDepartmentApp
{
    /// <summary>
    /// Логика взаимодействия для StaffingPage.xaml
    /// </summary>
    public partial class StaffingPage : System.Windows.Controls.Page
    {
        public StaffingPage()
        {
            InitializeComponent();
            staffing = new List<Staffing>();

            SortComboBox.Items.Add("Без сортировки");
            SortComboBox.Items.Add("Итог (по возрастанию)");
            SortComboBox.Items.Add("Итог (по убыванию)");
            SortComboBox.SelectedIndex = 0;
        }
        List<Staffing> staffing;

        private void Update(string sort = "")
        {
            var data = HumanResourcesDepartmentEntities1.GetContext().Staffing.ToList();

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrWhiteSpace(sort))
            {
                if (sort == "Без сортировки")
                {
                    data = data.OrderBy(c => c.Id_Staffing).ToList();
                }
                if (sort == "Итог (по возрастанию)")
                {
                    data = data.OrderBy(c => c.In_All).ToList();
                }
                if (sort == "Итог (по убыванию)")
                {
                    data = data.OrderByDescending(c => c.In_All).ToList();
                }
            }

            DGStaffing.ItemsSource = data;
        }

           

        private void FiltComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void BtnStaffing_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditStaffingPage((Staffing)DGStaffing.Items[DGStaffing.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditStaffingPage(null));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text;

            staffing = HumanResourcesDepartmentEntities1.GetContext().Staffing.ToList();
            DGStaffing.ItemsSource = staffing.Where(c => c.Position.Name.Contains(search));
            staffing = HumanResourcesDepartmentEntities1.GetContext().Staffing.Where(c => c.Position.Name.Contains(search)).ToList();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGStaffing.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Staffing.ToList();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var StaffingForRemoving = DGStaffing.SelectedItems.Cast<Staffing>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {StaffingForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities1.GetContext().Staffing.RemoveRange(StaffingForRemoving);
                    HumanResourcesDepartmentEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGStaffing.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Staffing.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                staffing.Count + 1, 7, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Специальность";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Количество сотрудников";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Оклад(руб)";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Надбавка за ночные смены (руб.)";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 5).Range.Text = "Премиальная надбавка (руб.)";
                wTable.Cell(1, 5).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 6).Range.Text = "Районный коэффициент";
                wTable.Cell(1, 6).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 7).Range.Text = "Итого (руб.)";
                wTable.Cell(1, 7).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in staffing)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.Position.Name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.Number_Of_Staff_Units.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.Salary.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.Night_Shift_Allowance.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 5).Range.Text = item.Premium.ToString();
                    wTable.Cell(countRow, 5).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 6).Range.Text = item.District_Coefficient.ToString();
                    wTable.Cell(countRow, 6).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 7).Range.Text = item.In_All.ToString();
                    wTable.Cell(countRow, 7).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update((SortComboBox.SelectedItem as String).ToString());
        }
    }
}