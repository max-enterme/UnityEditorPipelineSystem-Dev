using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class TestCollectionPipeline
{
    public class TaskCollection : ITaskCollection
    {
        private readonly bool when;
        private readonly IReadOnlyCollection<ITask> tasks;

        public TaskCollection(bool when, IReadOnlyCollection<ITask> tasks)
        {
            this.when = when;
            this.tasks = tasks ?? Array.Empty<ITask>();
        }

        public int GetTaskCount()
        {
            return tasks
                .Select(x => x switch
                {
                    ITaskCollection taskCollection => 1,
                    _ => 1
                })
                .Sum();
        }

        public bool When(IContextContainer _) => when;

        public IEnumerable<ITask> EnumerateTasks()
        {
            return tasks;
        }

        public Task PostAsync(IContextContainer _)
        {
            return Task.Run(() =>
            {
                Debug.Log("Post Porcess");
            });
        }
    }

    [MenuItem("Pipeline/TestCollectionPipeline/RunAsync")]
    public static async void RunAsync()
    {

        var dumpContext1 = new DumpContext
        {
            Message = "Test 1",
        };

        var dumpContext2 = new DumpContext
        {
            Message = "Test 2",
        };

        var dumpContext3 = new DumpContext
        {
            Message = "Test 3",
        };

        var contextContainer = new ContextContainer();
        contextContainer.SetContext<IDumpContext>(dumpContext1, "context1");
        contextContainer.SetContext<IDumpContext>(dumpContext2, "context2");
        contextContainer.SetContext<IDumpContext>(dumpContext3, "context3");

        var tasks = new ITask[]
        {
            DumpAsyncTask.CreateTask("context1"),
            new TaskCollection(true,
            new ITask[] {
                DumpAsyncTask.CreateTask("context2"),
                new TaskCollection(true,
                    new ITask[] {
                        DumpAsyncTask.CreateTask("context2"),
                        DumpAsyncTask.CreateTask("context3"),
                    }),
                new TaskCollection(false,
                    new ITask[] {
                         DumpAsyncTask.CreateTask("context2"),
                         DumpAsyncTask.CreateTask("context3"),
                    })})
        };

        using (var pipeline = new Pipeline(nameof(TestCollectionPipeline), contextContainer, tasks))
        {
            pipeline.PipelineLoggerFactory = UnityPipelineLogger.GetDefaultPipelineLoggerFactory(pipeline);
            await pipeline.RunAsync();
        }
    }
}
