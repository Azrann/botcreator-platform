namespace Bot.telegram.funnel.Actions
{
    public class ImageActionAsync : FunnelAction
    {
        public string FileName { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public bool RemoveKeyboard { get; set; }
        public int Delay { get; set; }

        public ImageActionAsync()
        {
        }

        public ImageActionAsync(string fileName, bool removePreviousMessage = true, int delay = 0, bool removeKeyboard = true)
        {
            FileName = fileName;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
            RemoveKeyboard = removeKeyboard;
        }
    }
}