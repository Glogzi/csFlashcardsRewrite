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
        public string[] QandA = File.ReadAllLines("QandA.txt");
        public MainWindow() {
            InitializeComponent();
            //creating QandA file if not exist
            doExist();
            string drawnedQ = randomQ(QandA);
            Question.Text = drawnedQ;
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
        private static void doExist() {
            if (!File.Exists("QandA.txt")){
                File.Create("QandA.txt");
                System.Diagnostics.Process.Start("flashcardsCsRewrite");
                Environment.Exit(0);
            }            
        }

        private void submit_Click(object sender, RoutedEventArgs e) {
            string showIt = Answer.Text.Trim();
            string drawnedQ = randomQ(QandA);
            Question.Text = drawnedQ;
        }
    }
}