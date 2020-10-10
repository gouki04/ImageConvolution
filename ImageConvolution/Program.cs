using CommandLine;

namespace ImageConvolution
{
    class Program
    {
        public class Options
        {
            [Option(shortName: 'i', longName: "input_file", Required = true, HelpText = "Image file to process.")]
            public string InputFile { get; set; }

            [Option(longName: "kernel", Required = false, Default = "All", HelpText = "Kernal Name, 'All' to process all kernels.")]
            public string KernelName { get; set; }

            [Option(longName: "edge", Required = false, Default = "Extend", HelpText = "Edge Handle Function.")]
            public string EdgeHandle { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => {
                    var image = new Image(o.InputFile);

                    if (o.EdgeHandle == "Extend") {
                        image.EdgeHandler = EdgeHandlers.Extend;
                    }
                    else if (o.EdgeHandle == "Wrap") {
                        image.EdgeHandler = EdgeHandlers.Wrap;
                    }
                    else if (o.EdgeHandle == "Mirror") {
                        image.EdgeHandler = EdgeHandlers.Mirror;
                    }
                    else {
                        throw new System.Exception($"Invaild Edge Handle: {o.EdgeHandle}");
                    }

                    if (o.KernelName == "All") {
                        foreach (var kernel in Kernel.Kernels) {
                            image.Process(kernel, $"{kernel.Name}-{o.EdgeHandle}.png");
                        }
                    }
                    else {
                        foreach (var kernel in Kernel.Kernels) {
                            if (kernel.Name == o.KernelName) {
                                image.Process(kernel, $"{kernel.Name}-{o.EdgeHandle}.png");
                            }
                        }
                    }
                });
        }
    }
}
