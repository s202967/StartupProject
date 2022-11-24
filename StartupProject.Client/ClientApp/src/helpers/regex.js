export const EmailRegexValidaion = (email) => {
  var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
};

export const PhoneNumberRegexValidation = (phone) => {
  var re = /^((\\+)|(00)|(\\*)|())[0-9]{3,14}((\\#)|())$/;
  return re.test(phone);
};

export const numberToNepaliCurrency = (number) => {
  let rst = parseFloat(number)
    .toFixed(2)
    // .replace(/\D/g, "")
    .replace(/(\d+?)(?=(\d\d)+(\d)(?!\d))(\.\d+)?/g, "$1,");

  return "Rs " + rst;
};

export const numberAndDecimals = (number) => {
  let re = /^([0-9]\d*)(\.\d+)?$/;
  return re.test(number);
};

export const numbersOnly = (number) => {
  let re = /^([0-9]\d*)?$/;
  return re.test(number);
};

export const positiveNumber = (number) => {
  let re = /^([1-9]\d*)?$/;
  return re.test(number);
};
