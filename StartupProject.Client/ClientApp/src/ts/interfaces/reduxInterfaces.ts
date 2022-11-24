export interface actionInterface {
  payload: any;
  type: string;
  tokenDetails: string;
}
export interface CommonAction {
  type: string;
  payload: any;
}
export interface validateAction {
  message: any;
  type: string;
  position: string;
  id: string;
}
