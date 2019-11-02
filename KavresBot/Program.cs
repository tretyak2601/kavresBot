using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramEmoji;
using FirebaseAdmin;


namespace KavresBot
{
    class Program
    {
        private static volatile List<UserInfo> users = new List<UserInfo>();
        private static TelegramBotClient bot;
        private const string tKey = "767958766:AAEyyBPHyzO_5HnwYF4wswJIP3Dn_PKmd1A";

        private static string oftenAnswer1 = "1 " + Emojis.Smileys_And_People.Body.Emotion.beating_heart;
        private static string oftenAnswer2 = "2 " + Emojis.Smileys_And_People.Body.Emotion.growing_heart;
        private static string oftenAnswer3 = ">2 " + Emojis.Smileys_And_People.Body.Emotion.red_heart;

        private static string restAnswer1 = "Зимний " + Emojis.Travel_And_Places.Sky_And_Weather.cloud;
        private static string restAnswer2 = "Летний " + Emojis.Travel_And_Places.Sky_And_Weather.sun;

        static void Main(string[] args)
        {
            bot = new TelegramBotClient(tKey);
            Telegram.Bot.Types.User me = bot.GetMeAsync().Result;

            bot.OnMessage += MessageHandler;
            bot.OnCallbackQuery += CallbackHandler;

            bot.StartReceiving();
            Console.ReadLine();
        }

        private async static void CallbackHandler(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {

        }

        private static async void MessageHandler(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            string userName = message.From.FirstName + " " + message.From.LastName;

            if (message.Type == MessageType.Text)
            {
                if (message.Text == "/start")
                {
                    Func<UserInfo, bool> func = new Func<UserInfo, bool>((user) => user.UserID == e.Message.From.Id);
                    int isExist = users.Where(func).Count();

                    if (isExist == 0)
                        users.Add(new UserInfo(e.Message.From.Id));

                    List<KeyboardButton> howOftenList = new List<KeyboardButton>();
                    howOftenList.Add(new KeyboardButton(oftenAnswer1));
                    howOftenList.Add(new KeyboardButton(oftenAnswer2));
                    howOftenList.Add(new KeyboardButton(oftenAnswer3));
                    var inline = new ReplyKeyboardMarkup(howOftenList);

                    await bot.SendTextMessageAsync(message.From.Id, e.Message.From.FirstName + ", приветствуем Вас!").
                        ContinueWith((task) => bot.SendTextMessageAsync(message.From.Id, "Как часто вы ездите отдыхать в год?", replyMarkup: inline));
                }
                if (message.Text == oftenAnswer1 || message.Text == oftenAnswer2 || message.Text == oftenAnswer3)
                {
                    var currentUser = users.Find(user => user.UserID == e.Message.From.Id);
                    currentUser.howOftenRest = message.Text;

                    List<KeyboardButton> rests = new List<KeyboardButton>();
                    rests.Add(new KeyboardButton(restAnswer1));
                    rests.Add(new KeyboardButton(restAnswer2));
                    var inline = new ReplyKeyboardMarkup(rests);

                    await bot.SendTextMessageAsync(message.From.Id, "Какой вид отдыха Вы предпочитаете?", replyMarkup: inline);
                }
                if (message.Text == restAnswer1 || message.Text == restAnswer2)
                {
                    var currentUser = users.Find(user => user.UserID == e.Message.From.Id);
                    currentUser.restType = message.Text;

                }
            }
        }
    }
}
