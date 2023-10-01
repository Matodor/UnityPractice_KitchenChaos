using System;

public interface IHasProgress
{
    public class ProgressChangedArgs : EventArgs
    {
        public float ProgressNormalized;
    }

    public event EventHandler<ProgressChangedArgs> OnProgressChanged;
}
