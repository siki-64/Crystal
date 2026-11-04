namespace Crystal.Input;

public sealed class InputMap<TAction> where TAction : struct, Enum
{
    private readonly Dictionary<TAction, List<InputBinding>> _bindings = [];

    public void Bind(TAction action, InputBinding binding)
    {
        if (binding.Kind == InputBindingKind.None) throw new ArgumentException("A binding needs a key, mouse button or gamepad button.", nameof(binding));
        if (!_bindings.TryGetValue(action, out var bindings)) _bindings[action] = bindings = [];
        if (!bindings.Contains(binding)) bindings.Add(binding);
    }

    public bool Unbind(TAction action, InputBinding binding) => _bindings.TryGetValue(action, out var bindings) && bindings.Remove(binding);

    public void Clear(TAction action) => _bindings.Remove(action);

    public IReadOnlyList<InputBinding> BindingsOf(TAction action) => _bindings.TryGetValue(action, out var bindings) ? bindings : [];

    public bool IsDown(InputFrame frame, TAction action) => AnyDown(frame, action, previous: false);

    public bool WasPressed(InputFrame frame, TAction action) => AnyDown(frame, action, previous: false) && !AnyDown(frame, action, previous: true);

    public bool WasReleased(InputFrame frame, TAction action) => !AnyDown(frame, action, previous: false) && AnyDown(frame, action, previous: true);

    private bool AnyDown(InputFrame frame, TAction action, bool previous)
    {
        if (!_bindings.TryGetValue(action, out var bindings)) return false;
        foreach (var binding in bindings)
            if (previous ? frame.WasDown(binding) : frame.IsDown(binding)) return true;
        return false;
    }
}
