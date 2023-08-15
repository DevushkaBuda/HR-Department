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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : System.Windows.Controls.Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            profile = new List<Profile>();
        }
        List<Profile> profile;
        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditProfilePage((Profile)DGProfile.Items[DGProfile.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditProfilePage(null));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text;

            profile = HumanResourcesDepartmentEntities1.GetContext().Profile.ToList();
            DGProfile.ItemsSource = profile.Where(c => c.Full_Name.Contains(search));
            profile = HumanResourcesDepartmentEntities1.GetContext().Profile.Where(c => c.Full_Name.Contains(search)).ToList();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGProfile.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Profile.ToList();      

            }
        }

        private void OtchetSotrud_Click(object sender, RoutedEventArgs e)
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
                profile.Count + 1, 9, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "ФИО";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Дата рождения";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Место рождения";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Место регистрации";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 5).Range.Text = "Информация о судимости";
                wTable.Cell(1, 5).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 6).Range.Text = "Номер телефона";
                wTable.Cell(1, 6).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 7).Range.Text = "Электронная почта";
                wTable.Cell(1, 7).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 8).Range.Text = "Данные паспорта";
                wTable.Cell(1, 8).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 9).Range.Text = "СНИЛС";
                wTable.Cell(1, 9).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in profile)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.Full_Name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.Date_of_Birth.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.Place_of_Birth.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.Place_of_Registration.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 5).Range.Text = item.Criminal_Record_Information.ToString();
                    wTable.Cell(countRow, 5).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 6).Range.Text = item.Phone_Number.ToString();
                    wTable.Cell(countRow, 6).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 7).Range.Text = item.Mail_Address.ToString();
                    wTable.Cell(countRow, 7).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 8).Range.Text = item.Passport_Data.ToString();
                    wTable.Cell(countRow, 8).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 9).Range.Text = item.SNILS.ToString();
                    wTable.Cell(countRow, 9).Range.Paragraphs.Alignment =
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
