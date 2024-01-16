using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace flashcardsCsRewrite {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public string correct = null;
        public MainWindow() {
            InitializeComponent();
            submit.Focus();
            //creating QandA file if not exist
            doExist();
            checkAnswer();
        }
        private static void doExist() {
            if (!File.Exists("QandA.txt")) {
                File.Create("QandA.txt").Close();
            }
        }
        private static string randomQ(string[] questionsList) {
            string drawnQuestion = null;
            try {
                while (drawnQuestion is null) {
                    Random rnd = new Random();
                    string[] onlyQ = new string[questionsList.Length];
                    //creating only question list
                    for (int question = 0; question < onlyQ.Length; question += 2) {
                        onlyQ[question] = questionsList[question];
                    }
                    //drawing random question
                    int index = rnd.Next(0, onlyQ.Length);
                    drawnQuestion = onlyQ[index];
                }
            }
            catch {
                drawnQuestion = "please Fill QandA.txt file";
            }

            return drawnQuestion;
        }
        private static string correctAnswer(string question, string[] QAlist) {
            string crrctAnswer;
            //crash handler when user will click too fast
            while (true) {
                try {
                    for (int line = 0; line < QAlist.Length; line++) {
                        if (QAlist[line] == question) {
                            crrctAnswer = QAlist[line + 1];
                            return crrctAnswer;
                        }
                    }
                    return "null";
                }
                catch {
                    question = randomQ(QAlist);
                    continue;
                }
            }
        }
        private void checkAnswer() {
            string[] QandA = File.ReadAllLines("QandA.txt");
            if (correct != null) {
                if (Answer.Text.ToLower() == correct) {
                    output.Text = "that's right!";
                    output.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
                else {
                    output.Text = $"nope, correct Asnwer is {correct}";
                    output.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            string drawnedQ = randomQ(QandA);
            Question.Text = drawnedQ;
            correct = correctAnswer(drawnedQ, QandA).ToLower();
            Answer.Text = "";
        }
        private void submit_Click(object sender, RoutedEventArgs e) {
            checkAnswer();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                checkAnswer();
            }
        }
    }
}