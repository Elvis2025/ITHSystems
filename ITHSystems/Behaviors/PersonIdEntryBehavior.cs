namespace ITHSystems.Behaviors;

public class PersonIdEntryBehavior : Behavior<Entry>
{

    private bool _updating;

    protected override void OnAttachedTo(Entry entry)
    {
        base.OnAttachedTo(entry);
        entry.TextChanged += OnTextChanged!;
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged!;
        base.OnDetachingFrom(entry);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_updating || sender is not Entry entry) return;

        var raw = e.NewTextValue ?? string.Empty;

        var digits = new string(raw.Where(char.IsDigit).ToArray());
        if (digits.Length > 11) digits = digits[..11];

        string formatted;
        if (digits.Length <= 3)
        {
            formatted = digits;
        }
        else if (digits.Length <= 10)
        {
            formatted = $"{digits[..3]}-{digits[3..]}";
        }
        else 
        {
            formatted = $"{digits[..3]}-{digits.Substring(3, 7)}-{digits[10]}";
        }

        if (formatted == raw) return;

        _updating = true;
        entry.TextChanged -= OnTextChanged!;

#if ANDROID
    var handler = entry.Handler as Microsoft.Maui.Handlers.EntryHandler;
    var editText = handler?.PlatformView as AndroidX.AppCompat.Widget.AppCompatEditText;
    if (editText is not null)
    {
        editText.EmojiCompatEnabled = false;
        editText.SetTextKeepState(formatted);
    }
    else
    {
        entry.Text = formatted;
    }
#else
        entry.Text = formatted;
#endif

        entry.Dispatcher.Dispatch(() =>
        {
            try
            {
#if !ANDROID
                var len = entry.Text?.Length ?? 0;
                entry.SelectionLength = 0;
                entry.CursorPosition = len;
#endif
            }
            finally
            {
                entry.TextChanged += OnTextChanged!;
                _updating = false;
            }
        });
    }
}
