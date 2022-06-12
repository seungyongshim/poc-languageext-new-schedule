using Cff.Effect;
using Cff.Effect.Abstractions;
using Cff.Effect.Json;
using Cff.Effect.Sha;
using LanguageExt.Common;

Error Failed = "I asked you to say hello, and you can't even do that?!";

using var ms = new MemoryStream("Hello\nHello\nHello\nnHello\nnHello\nnHello\nnHello\nhello\n".ToCharArray().Select(x => (byte)x).ToArray());
using var sr = new StreamReader(ms);

var q = (from __ in unitAff
         from _1 in Console.Out.WriteLineAsync("Say hello").ToUnit().ToAff()
         from _2 in sr.ReadLineAsync().ToAff()
         from _3 in Sha<RT>.SerializeEff(_2)
         from _4 in Console.Out.WriteLineAsync(_3.ToString()).ToUnit().ToAff()
         from _5 in guard(_2 == "hello", Failed)
         from _6 in Console.Out.WriteLineAsync("Hi").ToUnit().ToAff()
         select unit).Retry(Schedule.recurs(10));

using var ct = new CancellationTokenSource();
await q.Run(new RT(ct));

public readonly record struct RT(CancellationTokenSource CancellationTokenSource)
    : HasCancelDefault<RT>,
      HasJson<RT>,
      HasSha512<RT>;
