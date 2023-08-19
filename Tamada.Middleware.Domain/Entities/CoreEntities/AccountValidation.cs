using System.ComponentModel;
using System.Text.Json.Serialization;


public class AccountValidation { 

   private bool _ac_stat_no_dr;
    private bool _ac_stat_block;
    private bool _ac_stat_dormant;
    private bool _ac_stat_frozen; 


 [JsonPropertyName("AccountNumber")]
  public string cust_ac_no { get; set; } = string.Empty;

 [JsonPropertyName("Blocked")]
    public bool ac_stat_block
    {
        get { return _ac_stat_block; }
        set
        {
            if (_ac_stat_block != value)
            {
                _ac_stat_block = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }
    }



  [JsonPropertyName("Frozen")]
    public bool ac_stat_frozen
    {
        get { return _ac_stat_frozen; }
        set
        {
            if (_ac_stat_frozen != value)
            {
                _ac_stat_frozen = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }
    }
  
   [JsonPropertyName("PND")]
    public bool ac_stat_no_dr
    {
        get { return _ac_stat_no_dr;}
        set
        {
            if (_ac_stat_no_dr != value)
            {
                _ac_stat_no_dr = value;
                OnPropertyChanged(nameof(IsValid));
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
                OnPropertyChanged(nameof(IsValid));
            }
        }
    }


    public bool IsValid => !(ac_stat_no_dr || ac_stat_dormant || ac_stat_frozen || ac_stat_block);



     public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
         
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         
    }
}