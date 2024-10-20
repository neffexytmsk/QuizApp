using Newtonsoft.Json;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace QuizApp
{
    /// <summary>
    /// Логика взаимодействия для CreateTest.xaml
    /// </summary>
    public partial class CreateTest : Page
    {
        private Test test;
        public CreateTest()
        {
            InitializeComponent();
            test = new Test();
        }
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text) &&
                string.IsNullOrWhiteSpace(Answer1TextBox.Text) &&
                string.IsNullOrWhiteSpace(CorrectAnswerTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var question = new Question
            {
                Text = QuestionTextBox.Text,
                Answers = new List<string>
                {
                    Answer1TextBox.Text,
                    Answer2TextBox.Text,
                    Answer3TextBox.Text
                },
                CorrectAnswer = CorrectAnswerTextBox.Text,
            };

            if((Answer1TextBox.Text == CorrectAnswerTextBox.Text) ||( Answer2TextBox.Text == CorrectAnswerTextBox.Text) || (Answer3TextBox.Text == CorrectAnswerTextBox.Text))
            {
                test.Questions.Add(question);
                MessageBox.Show("Вопрос добавлен!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Введите правильный ответ");
            }
        }

        private void SaveTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название теста.");
                return;
            }
            test.Title = TitleTextBox.Text;

            List<Test> existingTests = new List<Test>();
            if (File.Exists("tests.json"))
            {
                var json = File.ReadAllText("tests.json");
                existingTests = JsonConvert.DeserializeObject<List<Test>>(json) ?? new List<Test>();
            }

            existingTests.Add(test);

            var updatedJson = JsonConvert.SerializeObject(existingTests, Formatting.Indented);
            File.WriteAllText("tests.json", updatedJson);

            MessageBox.Show($"Тест '{test.Title}' сохранен!");
            Frame.Navigate(new GoTest());
        }

        private void ClearFields()
        {
            QuestionTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
            CorrectAnswerTextBox.Clear();
        }
    }
}
