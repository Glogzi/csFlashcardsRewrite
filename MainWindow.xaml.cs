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
        }
        public static string randomQ(string[] questionsList) {
            Random rnd = new Random();
            string[] onlyQ = new string[questionsList.Length / 2];
            //creating only question list
            for (int question = 0; question < questionsList.Length; question += 2) {
                onlyQ[question] = questionsList[question];
            }
            //drawing random question
            int index = rnd.Next(0, onlyQ.Length);
            string drawnQuestion = onlyQ[index];
            return drawnQuestion;
        }
        public static void doExist() {
            if (!File.Exists("QandA.txt")){
                File.Create("QandA.txt");
                System.Diagnostics.Process.Start("flashcardsCsRewrite");
                Environment.Exit(0);
            }            
        }

        private void submit_Click(object sender, RoutedEventArgs e) {
            string showIt = Answer.Text.Trim();
            Question.Text = showIt;
        }
    }
}