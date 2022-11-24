export interface basicInputInterface {
  name: string;
  value?: any;
  onChange?: Function;
  label?: string;
  placeholder?: string;
  size?: string;
  width?: string;
  disabled?: boolean;
  onKeyUp?: Function;
  onBlur?: Function;
  onEnterPress?: Function;
  options?: any[];
  error?: any;
  className?: string;
}

export interface otherInputInterface extends basicInputInterface {
  minValue?: string;
  maxValue?: string;
  type?: string;
  autoFocus?: boolean;
  innerLabel?: string;
}

export interface otherTimePickerInterface extends basicInputInterface {
  time?: string;
  defaultMeridiem?: string;
}
export interface reactSelectInterface extends basicInputInterface {
  isArray?: boolean;
  isArrayKeys?: boolean;
  isAbove?: boolean;
  multiple?: boolean;
  loading?: boolean;
  loadingType?: string;
}

export interface checkRadioInterface extends basicInputInterface {
  checked?: boolean;
  labelPosition?: string;
}

export interface dateRangeInterface extends otherInputInterface {
  startDate?: any;
  endDate?: any;
}

export interface textEditorInterface extends basicInputInterface {
  toolbarId?: string;
}

export interface singleDocsInterface extends basicInputInterface {
  btnText?: string;
  extraClassName?: string;
}

export interface ydmInputCommonInterface extends basicInputInterface {
  monthValue?: string | number;
  yearValue?: string | number;
  dayValue?: string | number;
  inputShow?: any;
  // inputShow?:string[] | 'all' | 'months' | 'years' | 'days',
}

export interface ydmWholeInterface extends ydmInputCommonInterface {
  value: ydmValueProps;
  inputShow?: string[] | "all" | "months" | "years" | "days";
}

interface ydmValueProps {
  month?: string | number;
  year?: string | number;
  day?: string | number;
}

export interface checkdGroupProps extends basicInputInterface {
  checkGroupArray?: any[];
  checkedGroupValue?: any[];
}

export interface wholeFormInterface
  extends singleDocsInterface,
    checkRadioInterface,
    reactSelectInterface,
    otherInputInterface,
    textEditorInterface,
    dateRangeInterface,
    checkdGroupProps,
    ydmInputCommonInterface {
  formName:
    | "textinput"
    | "textarea"
    | "reactselect"
    | "dateInput"
    | "switch"
    | "dragAndDropUpload"
    | "radiogroup"
    | "searchableSelect"
    | "checkBoxGroup"

  alignment?: string;
  validators?: any;
  chipsArray?: any;
  isNepali?: boolean;
  badgeColor?: string;
  isDateRange?: boolean;
  minDate?: string;
  maxDate?: string;
  isFullDetails?: boolean;
  isTextInNepali?: boolean;
  validExtensions?: any;
  isManual?: any;
}
