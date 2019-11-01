using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;


namespace KavresBot
{
    class Program
    {
        private static TelegramBotClient bot;
        private const string tKey = "767958766:AAEyyBPHyzO_5HnwYF4wswJIP3Dn_PKmd1A";

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
            switch (e.CallbackQuery.Data)
            {
                case "@kavres":
                    await bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "You contant to Dmitriy");
                    break;
                case "@tretyakoff_b":
                    await bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "You contant to Bogdan");
                    break;
                default:
                    break;
            }
        }

        private static async void MessageHandler(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            string userName = message.From.FirstName + " " + message.From.LastName;

            if (message.Type == MessageType.Text)
            {
                if (message.Text == "/start")
                {
                    List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                    buttons.Add(InlineKeyboardButton.WithUrl("@kavres", "https://t.me/kavres"));
                    buttons.Add(InlineKeyboardButton.WithUrl("@tretyakoff_b", "https://t.me/tretyakoff_b"));
                    var inline = new InlineKeyboardMarkup(buttons);
                    await bot.SendTextMessageAsync(message.From.Id, "Choose contact", replyMarkup: inline);
                }
            }
        }
    }
}
