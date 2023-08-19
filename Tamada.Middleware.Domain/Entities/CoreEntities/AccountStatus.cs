using System.ComponentModel;
using System.Text.Json.Serialization;

public class AccountStatus : INotifyPropertyChanged
{
    #region <private>

    private bool _ac_stat_no_dr;
    private bool _ac_stat_block;
    private bool _ac_stat_dormant;
    private bool _ac_stat_frozen;

    #endregion

    [JsonPropertyName("AccountNumber")]
    public string? cust_ac_no { get; set; }

    [JsonPropertyName("PND")]
    public bool ac_stat_no_dr
    {
        get { return _ac_stat_no_dr;}
        set
        {
            if (_ac_stat_no_dr != value)
            {
                _ac_stat_no_dr = value;
                OnPropertyChanged(nameof(IssuesOnAccount), nameof(Active));
            }
        }
    }

    [JsonPropertyName("PND_Reason")]
    public string description { get; set; } = string.Empty;

    [JsonPropertyName("PNDReasonOpen")]
    public bool record_stat { get; set; }

    [JsonIgnore]
    [JsonPropertyName("Blocked")]
    public bool ac_stat_block
    {
        get { return _ac_stat_block; }
        set
        {
            if (_ac_stat_block != value)
            {
                _ac_stat_block = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }

    [JsonPropertyName("Dormant")]
    public bool ac_stat_dormant
    {
        get { return _ac_stat_dormant; }
        set
        {
            if (_ac_stat_dormant != value)
            {
                _ac_stat_dormant = value;
                OnPropertyChanged(nameof(IssuesOnAccount), nameof(Active));
            }
        }
    }
    [JsonIgnore]
    [JsonPropertyName("Frozen")]
    public bool ac_stat_frozen
    {
        get { return _ac_stat_frozen; }
        set
        {
            if (_ac_stat_frozen != value)
            {
                _ac_stat_frozen = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }
    [JsonIgnore]
    [JsonPropertyName("Status")]
    public string ACC_STATUS { get; set; } = string.Empty;

    public bool Active => !(ac_stat_no_dr || ac_stat_dormant || ac_stat_frozen || ac_stat_block);

    public string IssuesOnAccount
    {
        get
        { 
            if (ac_stat_no_dr && ac_stat_dormant)
                return "PND,Dormant";
            else if (ac_stat_no_dr)
                return "PND";
            else if (ac_stat_dormant)
                return "Dormant";
            else
                return string.Empty;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}
