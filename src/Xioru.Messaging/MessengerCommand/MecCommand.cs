﻿using Orleans;
using System.Text;
using Xioru.Messaging.Contracts.Command;
using Xioru.Messaging.Contracts.Formatting;
using Xioru.Messaging.Contracts.Messenger;
using Xioru.Messaging.Messenger;

namespace Xioru.Messaging.MessengerCommand
{
    public class MecCommand : BaseMessengerCommand
    {
        public const string UsageConst = "/mec";

        public MecCommand(IGrainFactory factory) : base(
            factory: factory,
            commandName: "mec",
            subCommandName: String.Empty,
            minArgumentsCount: 0,
            maxArgumentsCount: 0,
            usage: UsageConst)
        {
        }

        protected override Task<CommandResult> ExecuteInternal(MessengerCommandContext context)
        {
            var fString = new FormattedString("ChatId: ", StringFormatting.Bold);
            fString.Append(context.ChatId);

            return Task.FromResult(CommandResult.Success(fString));
        }
    }
}
