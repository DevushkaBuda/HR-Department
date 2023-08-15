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
namespace HumanResourcesDepartmentApp
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
            dayOfTheWeek = new DayOfTheWeek();
            shedule = new List<Schedule>(); 
            CBSearch.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Profile.ToList();
            DGSchedule.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Schedule.ToList();

        }
        DayOfTheWeek dayOfTheWeek;
        List <Schedule> shedule;
        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditSchedulePage((Schedule)DGSchedule.Items[DGSchedule.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditSchedulePage(null));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (CBSearch.Text != "")
            {
                int search = Convert.ToInt32(CBSearch.SelectedValue);
                if (Visibility == Visibility.Visible)
                {
                    HumanResourcesDepartmentEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGSchedule.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Schedule.Where(u => u.Id_Profile == search).ToList();
                    shedule = HumanResourcesDepartmentEntities1.GetContext().Schedule.Where(u => u.Id_Profile == search).ToList();
                }
            }
            else MessageBox.Show("Выберите специалиста!");
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var ScheduleForRemoving = DGSchedule.SelectedItems.Cast<Schedule>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {ScheduleForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities1.GetContext().Schedule.RemoveRange(ScheduleForRemoving);
                    HumanResourcesDepartmentEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    Search_Click(sender,e);
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
                shedule.Count + 1, 4, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Дата";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Статус";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Время работы";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Цех";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
               foreach (var item in shedule)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.DayOfTheWeek.Name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.Status.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.Duration.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.Cabinet.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
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
    }
}
