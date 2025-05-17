using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace Hw3o
{
    public partial class Default : System.Web.UI.Page
    {
        
        [Serializable]
        public class WordItem
        {
            public string Name { get; set; }
            public string Question { get; set; }
        }

        
        public class LetterItem
        {
            public string Letter { get; set; }
        }

        
        private static List<WordItem> Words;
        private string selectedWord;
        private char[] currentDisplay;
        private int wrongGuesses;
        private const int maxWrongGuesses = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadWords(); 
                BindLetters(); 
            }
        }

        private void LoadWords()
        {
            string filePath = Server.MapPath("~/App_Data/word.json");
            string json = File.ReadAllText(filePath);
            Words = JsonConvert.DeserializeObject<List<WordItem>>(json);
        }

        private void BindLetters()
        {
            List<LetterItem> letters = new List<LetterItem>();

            
            string turkishAlphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

            foreach (char c in turkishAlphabet)
            {
                letters.Add(new LetterItem { Letter = c.ToString() });
            }

            rptLetters.DataSource = letters;
            rptLetters.DataBind();
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            wrongGuesses = 0;
            ViewState["wrongGuesses"] = wrongGuesses;
            ResetHangmanImage();

           
            Random rnd = new Random();
            WordItem word = Words[rnd.Next(Words.Count)];
            selectedWord = word.Name.ToUpper();
            lblQuestion.Text = word.Question;

            
            currentDisplay = new char[selectedWord.Length];
            for (int i = 0; i < selectedWord.Length; i++)
            {
                currentDisplay[i] = (selectedWord[i] == ' ') ? ' ' : '_';
            }
            lblWord.Text = string.Join(" ", currentDisplay);
            lblMessage.Text = "";

            ViewState["selectedWord"] = selectedWord;
            ViewState["currentDisplay"] = currentDisplay;

            
            ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", "startTimer();", true);

           
            foreach (RepeaterItem item in rptLetters.Items)
            {
                Button btn = item.FindControl("btnLetter") as Button;
                if (btn != null)
                {
                    btn.Enabled = true;
                    btn.CssClass = "letter-button";
                }
            }
        }

        protected void rptLetters_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            selectedWord = ViewState["selectedWord"] as string;
            currentDisplay = ViewState["currentDisplay"] as char[];
            wrongGuesses = ViewState["wrongGuesses"] != null ? (int)ViewState["wrongGuesses"] : 0;

            if (string.IsNullOrEmpty(selectedWord) || currentDisplay == null)
            {
                lblMessage.Text = "Lütfen önce oyunu başlatın.";
                return;
            }

            string guessedLetter = e.CommandArgument.ToString().ToUpper();
            bool correct = false;
            for (int i = 0; i < selectedWord.Length; i++)
            {
                if (selectedWord[i] == guessedLetter[0])
                {
                    currentDisplay[i] = selectedWord[i];
                    correct = true;
                }
            }
            lblWord.Text = string.Join(" ", currentDisplay);

            Button btn = e.Item.FindControl("btnLetter") as Button;
            if (btn != null)
            {
                btn.Enabled = false;
                btn.CssClass += " disabled";
            }

            if (!correct)
            {
                wrongGuesses++;
                ViewState["wrongGuesses"] = wrongGuesses;
                UpdateHangmanImage(wrongGuesses);

                
                ScriptManager.RegisterStartupScript(this, GetType(), "balloonPopSound", "playBalloonPopSound();", true);
            }

            ViewState["currentDisplay"] = currentDisplay;

            
            if (new string(currentDisplay).Equals(selectedWord, StringComparison.OrdinalIgnoreCase))
            {
                lblMessage.Text = "Tebrikler! Kelimeyi doğru bildiniz: " + selectedWord;
                DisableAllLetterButtons();
                ScriptManager.RegisterStartupScript(this, GetType(), "gameOverSound", "stopTimer(); playGameOverSound('win');", true);
                btnStart.Enabled = true;
            }

            
            if (wrongGuesses >= maxWrongGuesses)
            {
                lblMessage.Text = "Maalesef, kaybettiniz! Doğru kelime: " + selectedWord;
                DisableAllLetterButtons();
                ScriptManager.RegisterStartupScript(this, GetType(), "gameOverSound", "playGameOverSound('lose'); stopTimer();", true);
                btnStart.Enabled = true;
            }
        }

        private void DisableAllLetterButtons()
        {
            foreach (RepeaterItem item in rptLetters.Items)
            {
                Button btn = item.FindControl("btnLetter") as Button;
                if (btn != null)
                    btn.Enabled = false;
            }
        }

        private void ResetHangmanImage()
        {
            imgBalloonMan.ImageUrl = "~/Images/balloonManFirst.png";
            
            imgBalloonMan.Style["top"] = "20px";
        }

        private void UpdateHangmanImage(int wrongCount)
        {
            
            int imageIndex = wrongCount + 1;
            imgBalloonMan.ImageUrl = "~/Images/balloonMan" + imageIndex + ".png?v=" + DateTime.Now.Ticks;

            
            int newTop = 20 + (wrongCount * 50);
            imgBalloonMan.Style["top"] = newTop + "px";
        }
    }
}
