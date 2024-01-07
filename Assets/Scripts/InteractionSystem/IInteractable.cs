namespace Spellsword
{
    public interface IInteractable
    {
        public bool isInteractable { get; set; }

        public string InteractionPrompt { get; }

        public bool Interact(InteractionSystem interactor);

        public void SetInteractable(bool value);
    }
}
