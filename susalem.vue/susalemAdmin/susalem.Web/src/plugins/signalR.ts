import * as SignalR from '@microsoft/signalr';
import { useNotification } from 'naive-ui';
import { localStg } from '@/utils/storage';

const baseURL = import.meta.env.VITE_SERVICE_Signarl_URL;
const notification = useNotification();
// 初始化SignalR对象
const connection = new SignalR.HubConnectionBuilder()
  .configureLogging(SignalR.LogLevel.Information)
  .withUrl(`${baseURL.replace('/api/', '')}/hub?token=${localStg.get('token')}`, {
    transport: SignalR.HttpTransportType.WebSockets,
    skipNegotiation: true
  })
  .withAutomaticReconnect({
    nextRetryDelayInMilliseconds: () => {
      return 5000; // 每5秒重连一次
    }
  })
  .build();

connection.keepAliveIntervalInMilliseconds = 15 * 1000; // 心跳检测15s
connection.serverTimeoutInMilliseconds = 30 * 60 * 1000; // 超时时间30m

// 启动连接
connection.start().then(() => {
  console.log('启动连接');
});
// 断开连接
connection.onclose(async () => {
  console.log('断开连接');
});
// 重连中
connection.onreconnecting(() => {
  notification.info({
    content: '提示',
    meta: '服务器已断线...',
    duration: 2500,
    keepAliveOnHover: true
  });
});
// 重连成功
connection.onreconnected(() => {
  console.log('重连成功');
});

connection.on('OnlineUserList', () => {});

export { connection as signalR };
