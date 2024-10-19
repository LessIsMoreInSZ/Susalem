type MenuTreeRes = ApiRes<{
  /** role name */
  name: string;
  /** role description */
  desc: string;
}>;

/** common record */
type ApiRes<T = any> = {
  /** record id */
  id: number;
  /** record creator */
  sysCreateUser: number;
  /** record create time */
  sysCreateTime: string;
  sysDeleteUser: number;
  sysDeleteTime: string;
  /** record updater */
  sysUpdateUser: string;
  /** record update time */
  sysUpdateTime: string;
  /** record status */
  status: number | null;
} & T;
