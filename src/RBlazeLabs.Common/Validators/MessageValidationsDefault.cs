namespace RBlazeLabs.Common.Validators
{

    /// <summary>
    /// Contains default messages for validations and errors
    /// </summary>
    internal static class MessageValidationsDefault
    {

        public static string STREET_NAME_REQUIRED => "Street name is required";
        public static string STREET_NAME_MIN_SIZE => "Street name must contain at least 2 characters";
        public static string STREET_NAME_MAX_SIZE => "Street name must contain a maximum of 80 characters";
        public static string ADDRESS_NUMBER_REQUIRED => "Address number is required";
        public static string ADDRESS_NUMBER_MAX_SIZE => "Address number must contain a maximum of 20 characters";
        public static string SECONDARY_ADDRESS_MAX_SIZE => "Secondary address must contain a maximum of 40 characters";
        public static string DISTRICT_REQUIRED => "District/Neighborhood is required";
        public static string DISTRICT_MIN_SIZE => "District/Neighborhood must contain at least 2 characters";
        public static string DISTRICT_MAX_SIZE => "District/Neighborhood must contain a maximum of 50 characters";
        public static string CITY_REQUIRED => "City is required";
        public static string CITY_MIN_SIZE => "City must contain at least 2 characters";
        public static string CITY_MAX_SIZE => "City must contain a maximum of 80 characters";
        public static string STATE_REQUIRED => "State is required";
        public static string STATE_SIZE => "State must contain exactly 2 positions";
        public static string ZIP_CODE_REQUIRED => "Zip code is required";
        public static string ZIP_COD_SIZE => "The zip code must contain exactly 8 positions";
        public static string ZIP_CODE_ONLY_NUMBERS => "Zip code is in an invalid format (enter numbers only)";
        public static string FIELD_REQUIRED => "The {0} field is required";
        public static string INVALID_MESSAGE => "The value entered for field {0} is invalid";
        public static string FIELD_INVALID => "{0} is invalid";
        public static string FIELD_MIN_SIZE => "The {0} field must contain at least {1} character(s)";
        public static string FIELD_MAX_SIZE => "The {0} fields must contain a maximum {1} character(s)";
        public static string EMAIL_INVALID => "Invalid e-mail";
        public static string DOCUMENT_REQUIRED => "{0} is required";
        public static string DOCUMENT_INVALID => "{0} invalid";
        public static string PAST_INVALID_DATE => "The '{0}' must be less than the current date";
        public static string FUTURE_INVALID_DATE => "The '{0}' must be greater than the current date";
        public static string START_DATE_INVALID => "[{0}] Invalid start date";
        public static string END_DATE_INVALID => "[{0}] Invalid end date";
        public static string DATES_INVALID => "[{0}] The end date must be greater than the start date";
        public static string FIRST_NAME_REQUIRED => "First name is required";
        public static string FIRST_NAME_MIN_SIZE => "First name must contain at least {0} characters";
        public static string FIRST_NAME_MAX_SIZE => "First name must contain a maximum of {0} characters";
        public static string FIRST_NAME_INVALID_CHARS => "First name contains invalid characters";
        public static string LAST_NAME_REQUIRED => "Last name is required";
        public static string LAST_NAME_MIN_SIZE => "Last name must contain at least {0} characters";
        public static string LAST_NAME_MAX_SIZE => "Last name must contain a maximum of {0} characters";
        public static string LAST_NAME_INVALID_CHARS => "Last name contains invalid characters";

    }
}
