using System;
using System.Threading.Tasks;
using OpenTracing;
using OpenTracing.Tag;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Microsoft.Extensions.Logging;
using OpenTracing.Util;
using System.Linq;
using System.Threading;

namespace jaegerpoker
{
    class Program
    {
        private ITracer _tracer;
        private Random _rand;

        static void Main(string[] args)
        {
            var prog = new Program();

             prog._tracer = new Tracer.Builder("JaegerPoker")
                .WithSampler(new ConstSampler(true))
                .Build();

            GlobalTracer.Register(prog._tracer);

            prog._rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                prog.RootSpan();
            }
        }

        private void RootSpan()
        {
            using (IScope scope = _tracer.BuildSpan("someWork").StartActive(finishSpanOnDispose: true))
            {
                try
                {
                    RandomSleep();

                    // Do some parallel children and wait for the result
                    MultiSpan();

                    // Call a sub method
                    ChildSpan();

                    // Do a nested call here cos we can
                    using (IScope scope2 = _tracer.BuildSpan("rootNested").StartActive(finishSpanOnDispose: true))
                    {
                        RandomSleep();
                    }
                }
                catch (Exception ex)
                {
                    Tags.Error.Set(scope.Span, true);
                }

                // No need to call scope.Span.Finish() as we've set finishSpanOnDispose:true in StartActive.
            }
        }

        private void ChildSpan()
        {
            using (IScope scope = _tracer.BuildSpan("thisChild").StartActive(finishSpanOnDispose: true))
            {
                try
                {
                    RandomSleep();

                    // Do a nested call here cos we can
                    using (IScope scope2 = _tracer.BuildSpan("childNested").StartActive(finishSpanOnDispose: true))
                    {
                        RandomSleep();
                    }
                }
                catch (Exception)
                {
                    Tags.Error.Set(scope.Span, true);
                }
            }
        }

        private void MultiSpan()
        {
            using (IScope scope = _tracer.BuildSpan("multiSpan").StartActive(finishSpanOnDispose: true))
            {
                var taskArray = new Task[10];
                for (int i = 0; i < taskArray.Length; i++)
                {
                    taskArray[i] = Task.Factory.StartNew((object obj) => {
                            int index = (int)obj;
                            MultiWork(index);
                        },
                        i);
                }
                Task.WaitAll(taskArray);
            }
        }

        private void MultiWork(int index)
        {
            using (IScope scope = _tracer.BuildSpan($"multiWork {index}").WithTag("A tag", $"SomeTagItem {index}").StartActive(finishSpanOnDispose: true))
            {
                RandomSleep();
            };
        }

        private void RandomSleep()
        {
            var number = _rand.Next(10, 100);

            System.Threading.Thread.Sleep(number);
        }
    }
}
