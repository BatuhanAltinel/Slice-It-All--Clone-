using System;

public class EventManager
{
    public static Action OnTap;

    public static Action OnStuck;
    public static Action OnPushBack;
    public static Action OnSlice;
    public static Action OnCombo;
    public static Action OnNotCombo;
    public static Action<KnifeState> OnKnifeStateChange;
    public static Action<int> OnObjectSliced;           // CurrencyAmount
    public static Action<int,int> OnCurrencyUpdated;    // Total Currency, LevelCurrency
    public static Action OnFail;  // When fail
    public static Action OnFinish; // When finish
}
