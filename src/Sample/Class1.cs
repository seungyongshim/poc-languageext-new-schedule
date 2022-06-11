using LanguageExt.Common;
using LanguageExt.Sys;
using RT = LanguageExt.Sys.Live.Runtime;

Error Failed = "I asked you to say hello, and you can't even do that?!";

var q = (from _ in Console<RT>.writeLine("Say hello")
         from t in Console.In.ReadLineAsync().ToAff()
         from e in guard(t == "hello", Failed)
         from m in Console<RT>.writeLine("Hi")
         select unit).Retry(Schedule.recurs(5));

await q.Run(RT.New());


