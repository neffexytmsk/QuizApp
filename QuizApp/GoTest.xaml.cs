using QuizApp.Models;
using System;
using System.Collections.Generic;
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

namespace QuizApp
{
    /// <summary>
    /// Логика взаимодействия для GoTest.xaml
    /// </summary>
    public partial class GoTest : Page
    {
        private List<Test> _tests;
        private int _currentTestIndex;
        private int _currentQuestionIndex;
        private int _score;
        public GoTest()
        {
            InitializeComponent();
            LoadTests();
        }

        private void LoadTests()
        {
            _tests = TestLoader.LoadTests("tests.json");
            TestSelector.ItemsSource = _tests;
            TestSelector.DisplayMemberPath = "Title";
        }

        private void TestSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TestSelector.SelectedItem is Test selectedTest)
            {
                _currentTestIndex = TestSelector.SelectedIndex;
                _currentQuestionIndex = 0;
                _score = 0;
                QuestionPanel.Visibility = Visibility.Visible;
                ResultText.Visibility = Visibility.Collapsed;
                ShowQuestion(selectedTest.Questions[_currentQuestionIndex]);
            }
        }

        private void ShowQuestion(Question question)
        {
            QuestionText.Text = question.Text;
            OptionsList.ItemsSource = question.Answers;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = OptionsList.SelectedItem as string;
            if (selectedOption != null)
            {
                var currentTest = _tests[_currentTestIndex];
                var currentQuestion = currentTest.Questions[_currentQuestionIndex];

                if (selectedOption == currentQuestion.CorrectAnswer)
                    _score++;

                _currentQuestionIndex++;

                if (_currentQuestionIndex < currentTest.Questions.Count)
                {
                    ShowQuestion(currentTest.Questions[_currentQuestionIndex]);
                }
                else
                {
                    ShowResult();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ответ.");
            }
        }
        private void ShowResult()
        {
            QuestionPanel.Visibility = Visibility.Collapsed;
            ResultText.Text = $"Ваш результат: {_score} из {_tests[_currentTestIndex].Questions.Count}";
            ResultText.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new CreateTest());
        }
    }
}
