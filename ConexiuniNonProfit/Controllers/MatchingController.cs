using Microsoft.AspNetCore.Mvc;
using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;
using Microsoft.AspNetCore.Identity;

namespace ConexiuniNonProfit.Controllers
{
    public class MatchingController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public MatchingController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        // Acțiunea pentru explorarea cărților
        public IActionResult Play()
        {
            var pieces = db.PuzzlePieces.ToList();
            return View(pieces);
        }

        // Acțiunea pentru quiz
        public IActionResult Quiz()
        {
            var questions = GetQuizQuestions();
            return View(questions);
        }

        private List<QuizQuestion> GetQuizQuestions()
        {
            return new List<QuizQuestion>
   {
       new QuizQuestion
       {
           Question = "Ce tip de activități preferi să desfășori?",
           Options = new List<string> {
               "Lucru direct cu beneficiarii",
               "Activități administrative",
               "Activități medicale",
               "Activități educaționale",
               "Activități de mediu",
               "Activități creative"
           }
       },
       new QuizQuestion
       {
           Question = "Care este principala cauză pentru care vrei să fii voluntar?",
           Options = new List<string> {
               "Dorința de a ajuta",
               "Dezvoltare personală",
               "Experiență profesională",
               "Impact în comunitate",
               "Networking",
               "Învățare continuă"
           }
       },
       new QuizQuestion
       {
           Question = "Ce grup vulnerabil dorești să sprijini?",
           Options = new List<string> {
               "Copii și tineri",
               "Vârstnici",
               "Persoane cu dizabilități",
               "Persoane cu boli grave",
               "Refugiați",
               "Comunități defavorizate"
           }
       },
       new QuizQuestion
       {
           Question = "Ce domeniu medical te atrage?",
           Options = new List<string> {
               "Oncologie pediatrică",
               "Donare de sânge",
               "Leucemie",
               "Donare de organe",
               "First aid",
               "Nu sunt interesat de domeniul medical"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de educație ai dori să oferi?",
           Options = new List<string> {
               "Educație formală (afterschool)",
               "Educație financiară",
               "Educație pentru sănătate",
               "Educație ecologică",
               "Educație civică",
               "Educație culturală"
           }
       },
       new QuizQuestion
       {
           Question = "Ce cauză de mediu te motivează cel mai mult?",
           Options = new List<string> {
               "Protecția animalelor",
               "Împăduriri",
               "Reciclare",
               "Conservarea biodiversității",
               "Educație ecologică",
               "Clean-up events"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de impact preferi să ai?",
           Options = new List<string> {
               "Impact imediat și direct",
               "Impact pe termen lung",
               "Impact la nivel de sistem",
               "Impact la nivel de comunitate",
               "Impact educațional",
               "Impact global"
           }
       },
       new QuizQuestion
       {
           Question = "Care este zona geografică în care preferi să activezi?",
           Options = new List<string> {
               "Urban",
               "Rural",
               "Zone defavorizate",
               "Zone de munte",
               "Zone costiere",
               "Oriunde este nevoie"
           }
       },
       new QuizQuestion
       {
           Question = "Ce abilități ai dori să dezvolți prin voluntariat?",
           Options = new List<string> {
               "Leadership",
               "Comunicare",
               "Organizare de evenimente",
               "Fundraising",
               "Project management",
               "Abilități digitale"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de organizație preferi?",
           Options = new List<string> {
               "ONG mare, internațional",
               "ONG local, mic",
               "Instituție publică",
               "Organizație de tineret",
               "Fundație medicală",
               "Organizație de mediu"
           }
       },
       new QuizQuestion
       {
           Question = "Ce probleme sociale te preocupă cel mai mult?",
           Options = new List<string> {
               "Sărăcia",
               "Discriminarea",
               "Accesul la educație",
               "Accesul la sănătate",
               "Drepturile omului",
               "Egalitatea de gen"
           }
       },
       new QuizQuestion
       {
           Question = "Care este disponibilitatea ta de timp?",
           Options = new List<string> {
               "Câteva ore pe săptămână",
               "Weekend-uri",
               "Full-time",
               "Sezonier",
               "Flexibil",
               "Pentru proiecte punctuale"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de activități culturale preferi?",
           Options = new List<string> {
               "Organizare evenimente culturale",
               "Tururi ghidate",
               "Ateliere creative",
               "Restaurare monumente",
               "Promovare culturală",
               "Documentare istorică"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de sport sau activitate fizică preferi?",
           Options = new List<string> {
               "Sport pentru copii",
               "Sport pentru persoane cu dizabilități",
               "Sport pentru vârstnici",
               "Evenimente sportive",
               "Promovarea sportului",
               "Activități în natură"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de proiecte tehnologice te interesează?",
           Options = new List<string> {
               "Educație digitală",
               "Dezvoltare software",
               "Suport IT pentru ONG-uri",
               "Robotică pentru copii",
               "Accessibilitate digitală",
               "Smart city"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de activism preferi?",
           Options = new List<string> {
               "Advocacy",
               "Campanii de conștientizare",
               "Petiții și lobby",
               "Acțiuni directe",
               "Activism digital",
               "Educație civică"
           }
       },
       new QuizQuestion
       {
           Question = "Ce grup de vârstă preferi să ajuți?",
           Options = new List<string> {
               "Copii (0-12 ani)",
               "Adolescenți (13-18 ani)",
               "Tineri (19-25 ani)",
               "Adulți",
               "Vârstnici",
               "Toate grupele de vârstă"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de susținere preferi să oferi?",
           Options = new List<string> {
               "Suport emoțional",
               "Suport educațional",
               "Suport medical",
               "Suport material",
               "Suport administrativ",
               "Suport tehnic"
           }
       },
       new QuizQuestion
       {
           Question = "Ce cauză internațională te motivează?",
           Options = new List<string> {
               "Refugiați",
               "Schimbări climatice",
               "Sărăcie extremă",
               "Educație globală",
               "Pace și reconciliere",
               "Dezvoltare durabilă"
           }
       },
       new QuizQuestion
       {
           Question = "Ce tip de evenimente preferi să organizezi?",
           Options = new List<string> {
               "Fundraising",
               "Conferințe și workshop-uri",
               "Evenimente comunitare",
               "Evenimente sportive",
               "Evenimente culturale",
               "Acțiuni de mediu"
           }
       }
   };
   }


        [HttpPost]
        public IActionResult CalculateMatch(Dictionary<string, string> answers)
        {
            var userId = _userManager.GetUserId(User);
            var scores = CalculateMatchScores(answers);

            var results = scores
                .Select(score => new QuizResult
                {
                    UserId = userId,
                    Category = score.Key,
                    MatchPercentage = CalculatePercentage(score.Value),
                    CreatedAt = DateTime.Now
                })
                .OrderByDescending(r => r.MatchPercentage)
                .Take(5)
                .ToList();

            db.QuizResults.AddRange(results);
            db.SaveChanges();

            return View("Results", results);
        }

        private Dictionary<string, int> CalculateMatchScores(Dictionary<string, string> answers)
        {
            var categoryScores = new Dictionary<string, int>
   {
       {"Cancer", 0},
       {"Leucemie", 0},
       {"Donare de Sânge", 0},
       {"Donare de Organe", 0},
       {"Voluntariat", 0},
       {"Educație", 0},
       {"Mediu", 0},
       {"Cultură", 0},
       {"Drepturile Omului", 0},
       {"Ajutor Umanitar", 0},
       {"Sănătate", 0},
       {"Animale", 0},
       {"Sport", 0},
       {"Tineret", 0},
       {"Femei", 0},
       {"Copii", 0},
       {"Vârstnici", 0},
       {"Persoane cu Dizabilități", 0},
       {"Comunități Defavorizate", 0},
       {"Refugiați", 0},
       {"Educație Financiară", 0}
   };

            foreach (var answer in answers)
            {
                switch (answer.Key)
                {
                    case "Ce tip de activități preferi să desfășori?":
                        switch (answer.Value)
                        {
                            case "Lucru direct cu beneficiarii":
                                categoryScores["Voluntariat"] += 20;
                                categoryScores["Ajutor Umanitar"] += 15;
                                break;
                            case "Activități medicale":
                                categoryScores["Sănătate"] += 20;
                                categoryScores["Cancer"] += 15;
                                categoryScores["Leucemie"] += 15;
                                break;
                            case "Activități educaționale":
                                categoryScores["Educație"] += 20;
                                categoryScores["Copii"] += 15;
                                break;
                            case "Activități de mediu":
                                categoryScores["Mediu"] += 20;
                                categoryScores["Animale"] += 15;
                                break;
                        }
                        break;

                    case "Ce grup vulnerabil dorești să sprijini?":
                        switch (answer.Value)
                        {
                            case "Copii și tineri":
                                categoryScores["Copii"] += 20;
                                categoryScores["Tineret"] += 20;
                                break;
                            case "Vârstnici":
                                categoryScores["Vârstnici"] += 25;
                                break;
                            case "Persoane cu dizabilități":
                                categoryScores["Persoane cu Dizabilități"] += 25;
                                break;
                            case "Persoane cu boli grave":
                                categoryScores["Cancer"] += 20;
                                categoryScores["Leucemie"] += 20;
                                categoryScores["Sănătate"] += 15;
                                break;
                            case "Refugiați":
                                categoryScores["Refugiați"] += 25;
                                categoryScores["Ajutor Umanitar"] += 15;
                                break;
                        }
                        break;

                    case "Ce domeniu medical te atrage?":
                        switch (answer.Value)
                        {
                            case "Oncologie pediatrică":
                                categoryScores["Cancer"] += 25;
                                categoryScores["Copii"] += 15;
                                break;
                            case "Donare de sânge":
                                categoryScores["Donare de Sânge"] += 25;
                                break;
                            case "Leucemie":
                                categoryScores["Leucemie"] += 25;
                                break;
                            case "Donare de organe":
                                categoryScores["Donare de Organe"] += 25;
                                break;
                        }
                        break;

                    case "Ce tip de educație ai dori să oferi?":
                        switch (answer.Value)
                        {
                            case "Educație formală":
                                categoryScores["Educație"] += 25;
                                break;
                            case "Educație financiară":
                                categoryScores["Educație Financiară"] += 25;
                                break;
                            case "Educație pentru sănătate":
                                categoryScores["Sănătate"] += 20;
                                categoryScores["Educație"] += 15;
                                break;
                            case "Educație ecologică":
                                categoryScores["Mediu"] += 20;
                                categoryScores["Educație"] += 15;
                                break;
                        }
                        break;

                    case "Ce cauză de mediu te motivează cel mai mult?":
                        switch (answer.Value)
                        {
                            case "Protecția animalelor":
                                categoryScores["Animale"] += 25;
                                categoryScores["Mediu"] += 15;
                                break;
                            case "Împăduriri":
                                categoryScores["Mediu"] += 25;
                                break;
                            case "Conservarea biodiversității":
                                categoryScores["Mediu"] += 20;
                                categoryScores["Animale"] += 15;
                                break;
                        }
                        break;

                    case "Ce probleme sociale te preocupă cel mai mult?":
                        switch (answer.Value)
                        {
                            case "Sărăcia":
                                categoryScores["Comunități Defavorizate"] += 25;
                                categoryScores["Ajutor Umanitar"] += 15;
                                break;
                            case "Discriminarea":
                                categoryScores["Drepturile Omului"] += 25;
                                break;
                            case "Accesul la educație":
                                categoryScores["Educație"] += 25;
                                break;
                            case "Accesul la sănătate":
                                categoryScores["Sănătate"] += 25;
                                break;
                            case "Drepturile omului":
                                categoryScores["Drepturile Omului"] += 25;
                                break;
                            case "Egalitatea de gen":
                                categoryScores["Femei"] += 25;
                                categoryScores["Drepturile Omului"] += 15;
                                break;
                        }
                        break;

                    case "Ce tip de sport sau activitate fizică preferi?":
                        switch (answer.Value)
                        {
                            case "Sport pentru copii":
                                categoryScores["Sport"] += 20;
                                categoryScores["Copii"] += 15;
                                break;
                            case "Sport pentru persoane cu dizabilități":
                                categoryScores["Sport"] += 20;
                                categoryScores["Persoane cu Dizabilități"] += 15;
                                break;
                            case "Sport pentru vârstnici":
                                categoryScores["Sport"] += 20;
                                categoryScores["Vârstnici"] += 15;
                                break;
                        }
                        break;

                    case "Ce tip de organizație preferi?":
                        switch (answer.Value)
                        {
                            case "ONG mare, internațional":
                                categoryScores["Ajutor Umanitar"] += 20;
                                categoryScores["Refugiați"] += 15;
                                break;
                            case "Fundație medicală":
                                categoryScores["Sănătate"] += 20;
                                categoryScores["Cancer"] += 15;
                                categoryScores["Leucemie"] += 15;
                                break;
                            case "Organizație de mediu":
                                categoryScores["Mediu"] += 20;
                                categoryScores["Animale"] += 15;
                                break;
                            case "Organizație de tineret":
                                categoryScores["Tineret"] += 20;
                                categoryScores["Educație"] += 15;
                                break;
                        }
                        break;

                    case "Ce tip de impact preferi să ai?":
                        switch (answer.Value)
                        {
                            case "Impact imediat și direct":
                                categoryScores["Ajutor Umanitar"] += 20;
                                categoryScores["Sănătate"] += 15;
                                break;
                            case "Impact educațional":
                                categoryScores["Educație"] += 20;
                                categoryScores["Educație Financiară"] += 15;
                                break;
                            case "Impact la nivel de comunitate":
                                categoryScores["Comunități Defavorizate"] += 20;
                                break;
                            case "Impact global":
                                categoryScores["Mediu"] += 15;
                                categoryScores["Drepturile Omului"] += 15;
                                break;
                        }
                        break;

                    case "Ce grup de vârstă preferi să ajuți?":
                        switch (answer.Value)
                        {
                            case "Copii (0-12 ani)":
                                categoryScores["Copii"] += 25;
                                break;
                            case "Adolescenți (13-18 ani)":
                                categoryScores["Tineret"] += 25;
                                break;
                            case "Vârstnici":
                                categoryScores["Vârstnici"] += 25;
                                break;
                        }
                        break;

                    case "Ce tip de susținere preferi să oferi?":
                        switch (answer.Value)
                        {
                            case "Suport emoțional":
                                categoryScores["Persoane cu Dizabilități"] += 15;
                                categoryScores["Vârstnici"] += 15;
                                break;
                            case "Suport medical":
                                categoryScores["Sănătate"] += 20;
                                categoryScores["Cancer"] += 15;
                                categoryScores["Leucemie"] += 15;
                                break;
                            case "Suport educațional":
                                categoryScores["Educație"] += 20;
                                categoryScores["Copii"] += 15;
                                break;
                        }
                        break;

                    case "Ce cauză internațională te motivează?":
                        switch (answer.Value)
                        {
                            case "Refugiați":
                                categoryScores["Refugiați"] += 25;
                                categoryScores["Ajutor Umanitar"] += 15;
                                break;
                            case "Schimbări climatice":
                                categoryScores["Mediu"] += 25;
                                break;
                            case "Educație globală":
                                categoryScores["Educație"] += 20;
                                break;
                            case "Pace și reconciliere":
                                categoryScores["Drepturile Omului"] += 20;
                                break;
                        }
                        break;

                    case "Ce tip de evenimente preferi să organizezi?":
                        switch (answer.Value)
                        {
                            case "Evenimente culturale":
                                categoryScores["Cultură"] += 25;
                                break;
                            case "Evenimente sportive":
                                categoryScores["Sport"] += 25;
                                break;
                            case "Acțiuni de mediu":
                                categoryScores["Mediu"] += 20;
                                categoryScores["Animale"] += 15;
                                break;
                            case "Evenimente comunitare":
                                categoryScores["Comunități Defavorizate"] += 20;
                                break;
                        }
                        break;

                    case "Ce tip de activism preferi?":
                        switch (answer.Value)
                        {
                            case "Advocacy":
                                categoryScores["Drepturile Omului"] += 20;
                                break;
                            case "Campanii de conștientizare":
                                categoryScores["Educație"] += 15;
                                categoryScores["Sănătate"] += 15;
                                break;
                            case "Educație civică":
                                categoryScores["Educație"] += 20;
                                categoryScores["Drepturile Omului"] += 15;
                                break;
                        }
                        break;

                    case "Ce tip de proiecte tehnologice te interesează?":
                        switch (answer.Value)
                        {
                            case "Educație digitală":
                                categoryScores["Educație"] += 20;
                                categoryScores["Tineret"] += 15;
                                break;
                            case "Accessibilitate digitală":
                                categoryScores["Persoane cu Dizabilități"] += 20;
                                break;
                            case "Robotică pentru copii":
                                categoryScores["Copii"] += 20;
                                categoryScores["Educație"] += 15;
                                break;
                        }
                        break;
                }
            }

            return categoryScores;
        }

        private int CalculatePercentage(int score)
        {
            const int maxPossibleScore = 100;
            return Math.Min(100, (score * 100) / maxPossibleScore);
        }
    }
}