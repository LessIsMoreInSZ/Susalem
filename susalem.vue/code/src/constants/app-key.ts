/** 设备类型 */
export enum DeviceEnum {
  Mobile,
  Desktop
}

/** 布局模式 */
export enum LayoutModeEnum {
  Left = "left",
  Top = "top",
  LeftTop = "left-top"
}

/** 侧边栏打开状态常量 */
export const SIDEBAR_OPENED = "opened"
/** 侧边栏关闭状态常量 */
export const SIDEBAR_CLOSED = "closed"

export const SINGNALR_USERID = "web_01004157"

export type SidebarOpened = typeof SIDEBAR_OPENED
export type SidebarClosed = typeof SIDEBAR_CLOSED

export const VAR_ADS_TEMPLATE_URL = "Resources/Excel/导入变量模板.xlsx"
export const VAR_ADS_TEMPLATE2_URL = "Resources/Excel/导入报警配置模板.xlsx"
