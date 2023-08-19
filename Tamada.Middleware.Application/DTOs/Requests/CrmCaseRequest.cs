 
using System.Text.Json.Serialization;
      
 
public class CrmCaseRequest{

        [JsonPropertyName("AccountNumber")]
        
        public string ab_bankaccount {get;set;} = string.Empty;
        [JsonPropertyName("CaseType")]
        public  string ab_casetype { get;set;} = string.Empty;
        [JsonPropertyName("Category")]
        public string ab_category {get;set;} = string.Empty;
        [JsonPropertyName("Category")]
        public string ab_subcategory {get;set;} = string.Empty;
        [JsonPropertyName("ConfirmFinancialImplication")]
        public string ab_confirmfinancialimplication {get;set;} = string.Empty;


        [JsonPropertyName("Description")]
        public string description {get;set;} = string.Empty;
        [JsonPropertyName("CaseOriginCode")]
        public string caseorigincode {get;set;} = string.Empty;
        [JsonPropertyName("TransactionDate")]
        public string ab_transaction_date {get;set;} = string.Empty;
        

    }
