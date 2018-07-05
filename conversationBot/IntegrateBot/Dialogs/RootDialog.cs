namespace IntegrateBots.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Web;
    using AutoMapper;
    using BotAssets.Extensions;
    using Microsoft.Bot.Builder.ConnectorEx;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Location;
    using Microsoft.Bot.Connector;
    using Properties;
    using IntegrateBots.Extensions;

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private readonly string checkoutUriFormat;

        private ConversationReference conversationReference;
        
        public RootDialog(string checkoutUriFormat)
        {
            this.checkoutUriFormat = checkoutUriFormat;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task WelcomeMessageAsync(IDialogContext context)
        {
            var reply = context.MakeMessage();          
            await context.PostAsync(reply);
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //if (this.conversationReference == null)
            //{
            //    this.conversationReference = message.ToConversationReference();
            //}
            #region ClientA - Keaton - LP

            if ((message.Text.Contains(Resources.RootDialog_clientA) == true))
            {
                string replyMsg = "I see you want to protect your credit score.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_name) == true))
            {
                string replyMsg = "Hi Keaton, I see you want to protect your credit score.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_affect) == true))
            {
                string replyMsg = "Your credit rating is used to determine what interest rate you pay for things like car loans and mortgages.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_protect) == true))
            {
                string replyMsg = "I would recommend Balance Protector, it helps to cover of the balance on your credit card if you become unable to work because of total disability or involuntary unemployment.  Also, there are no premiums unless there is an unpaid balance.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_disabled) == true))
            {
                string replyMsg = "Not quite, we would pay you monthly instalments equal to 10% of your balance owed, and you would only have to pay 1.19%.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_currentBalance) == true))
            {
                string replyMsg = "Based on your current VISA balance of $42.78, we would pay $4.28 per month until the balance is fully paid. You would only pay $0.51 per month.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Keaton_sign) == true))
            {
                string replyMsg = "Thanks for choosing Balance Protector, you are now covered on account (7891)";
                await this.PlainText(context, replyMsg);
            }

            #endregion

            #region CLient B - House & Mortgagae Check
            else if ((message.Text.Contains(Resources.RootDialog_clientB) == true))
            {
                string replyMsg = "Hello Ingrid, we see that you are looking at buying a new home. What can I help you with?";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOUSElist_mortgage) == true))
            {
                string replyMsg = "Tell me more about what you are looking for in a home";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_House_Markham) == true))
            {
                string replyMsg = "I will review your mortgage application again. In the meantime time I would like to share some houst listing I have found for you. Do you want to take a look?";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOUSElist_yes) == true))
            {
                await this.HouseList(context, result);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOUSElist_budget) == true))
            {
                string replyMsg = "We also have a network of realtors we can leverage that provided our clients with preferred rates.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOUSElist_process) == true))
            {
                string replyMsg = "Yes we can, you can schedule an appointment through me at any RBC Banking Branch, just pick a time and a place.";
                await this.PlainText(context, replyMsg);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOUSElist_branch) == true))
            {  
                string replyMsg = "It has been booked. We will be sure to help you plan for an protect your future.";
                await this.PlainText(context, replyMsg);
            }
            #endregion

            #region CLient C - HP
            else if ((message.Text.Contains(Resources.RootDialog_clientC) == true))
            {
                await this.HPList(context, result);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HP_calc) == true))
            {
                await this.HPList(context, result);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Leo_why) == true))
            {
                string replyMsg = "Home Protector helps to ensure that your mortgage will be paid if you pass away or become disabled.";
                //Need Chinese 
                String finalTranslate = await Translate.TranslateCH(replyMsg);
                await this.PlainText(context, finalTranslate);
            }
            else if ((message.Text.Contains(Resources.RootDialog_Leo_important) == true))
            {
                string replyMsg = "This product is very useful, even the CEO of RBC Insurance was saved by this insurance when he was young and his dad passed away.  The mortgage was paid off and Neil was able to live in continue living in his home.";
                //Need Chinese 
                String finalTranslate = await Translate.TranslateCH(replyMsg);
                await this.PlainText(context, finalTranslate);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOME_buy) == true))
            {
                string replyMsg = "Thank you for choosing our product, I just need to ask you a few health questions to determine which coverage is best for you and make sure you qualify.";
                //Need Chinese 
                String finalTranslate = await Translate.TranslateCH(replyMsg);
                await this.PlainText(context, finalTranslate);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOME_buy_Q1) == true))
            {
                string replyMsg = "Do you have any pre-existing health condition?";
                //Need Chinese 
                String finalTranslate = await Translate.TranslateCH(replyMsg);
                await this.PlainText(context, finalTranslate);
            }
            else if ((message.Text.Contains(Resources.RootDialog_HOME_buy_NO) == true))
            {
                string replyMsg = "Congrats. Your purchase has been completed and an email receipt has been sent to you.";
                await this.PlainText(context, replyMsg);
            }
            else
            {
                await this.PlainText(context, Resources.RootDialog_Chat_Error);
            }

            #endregion
        }       

        private async Task PlainText(IDialogContext context, string text)
        {
            var reply = context.MakeMessage();
            reply.Text = text;
            await context.PostAsync(reply);
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task StartOverAsync(IDialogContext context, IMessageActivity message)
        {
            await this.WelcomeMessageAsync(context);
        }

        #region Cards

        private async Task HPList(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetHPAttachments();

            await context.PostAsync(reply);

            context.Wait(this.MessageReceivedAsync);
        }

        private static IList<Attachment> GetHPAttachments()
        {
            return new List<Attachment>()
            {
               GetThumbnailCard(
                    "Life Coverage",
                    "$39.70/month",
                    "Pays off or reduces your outstanding mortgage balance in the event of death (up to $750,000).",
                    new CardImage(url: "https://hackathon4.azureedge.net/cdn/images/CI.PNG"),
                    new CardAction(ActionTypes.OpenUrl, "Add protection", value: "https://www.rbcinsurance.com/loan-mortgage-credit-protection/index.html")),
              GetThumbnailCard(
                    "Life & Disability Coverage",
                    "$57.40/month",
                    "Pays your regular mortgage payment(s) in the event of a disability (up to $3,000 per month for a maximum of 24 months).",
                    new CardImage(url: "https://hackathon4.azureedge.net/cdn/images/DI.PNG"),
                    new CardAction(ActionTypes.OpenUrl, "Qualify now", value: "https://www.rbcinsurance.com/loan-mortgage-credit-protection/index.html")),
                GetThumbnailCard(
                    "Life & Critical Illness Coverage",
                    "$64.70/month",
                    "Pays off your outstanding mortgage(s) balance if you are diagnosed with a covered illness (up to $300,000).",
                    new CardImage(url: "https://hackathon4.azureedge.net/cdn/images/Life.PNG"),
                    new CardAction(ActionTypes.OpenUrl, "Protect now", value: "https://www.rbcinsurance.com/loan-mortgage-credit-protection/index.html")),

            };
        }

        private async Task HouseList(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetHouseCardsAttachments();

            await context.PostAsync(reply);

            context.Wait(this.MessageReceivedAsync);
        }

        private static IList<Attachment> GetHouseCardsAttachments()
        {
            return new List<Attachment>()
            {
               GetHeroCard(
                    "3 Bdrm Townhouse in Markham",
                    "$609,000",
                    "Beautiful 3 bedroom, 2 bath townhome near L3T. MLS: N4150941",
                    //new CardImage(url: "https://hackathon4.azureedge.net/cdn/images/Home1.jpg"),
                    new CardImage("D:/Coding/CarouselCard/cards-CarouselCards/images/Home1.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "Check Detail", value: "https://www.realtor.ca/Residential/Single-Family/19528995/215--320-JOHN-ST-Markham-Ontario-L3T0B1-Aileen-Willowbrook")),
               GetHeroCard(
                    "3 Bdrm Townhome in Markkam",
                    "$649,800",
                    "eautiful 3 bedroom, 2 bath townhome near L4C. MLS: N4113184",
                    //new CardImage(url: "https://hackathon4.azureedge.net/cdn/images/Home2.jpg"),
                    new CardImage("D:/Coding/CarouselCard/cards-CarouselCards/images/Home2.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "Check Detail", value: "https://www.realtor.ca/Residential/Single-Family/19370359/9--260-AVENUE-RD-Richmond-Hill-Ontario-L4C5G6-North-Richvale")),
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var thumbnailCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return thumbnailCard.ToAttachment();
        }
    
    #endregion
}
}