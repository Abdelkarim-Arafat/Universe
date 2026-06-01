using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.SignalR.Common;

public static class MessageEvents
{
    public const string Received = "MessageReceived";
    public const string Replied = "MessageReplied";
    public const string Read = "MessageRead";
}