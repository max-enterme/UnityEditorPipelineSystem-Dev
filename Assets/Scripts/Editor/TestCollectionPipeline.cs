using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEditorPipelineSystemDev.Editor.Tasks;

public class TestCollectionPipeline
{
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
            new TaskCollection(
            new ITask[] {
                DumpAsyncTask.CreateTask("context2"),
                new TaskCollection(
                    new ITask[] {
                        DumpAsyncTask.CreateTask("context2"),
                        DumpAsyncTask.CreateTask("context3"),
                    }),
                new TaskCollection(
                    new ITask[] {
                         DumpAsyncTask.CreateTask("context2"),
                         DumpAsyncTask.CreateTask("context3"),
                    })})
        };

        await Utility.RunAsync(nameof(TestCollectionPipeline), contextContainer, tasks);
    }
}
