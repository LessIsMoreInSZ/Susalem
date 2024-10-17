import * as singnalr from "@microsoft/signalr"
import { ElMessage } from "element-plus"
const baseApi = import.meta.env.VITE_BASE_API
let timer: any = null
const timeLength: number = 5000

let connectionSignalR = new singnalr.HubConnectionBuilder()
  .withUrl(baseApi + "/hubs", {
    skipNegotiation: true,
    transport: singnalr.HttpTransportType.WebSockets
  })
  .withAutomaticReconnect([1000, 2000, 5000, 10000, 30000])
  .configureLogging(singnalr.LogLevel.Error)
  .build()

// connectionSignalR.onclose((error) => {
//   ElMessage.warning("服务器websocket连接关闭,尝试重连...")
//   timer = setInterval(() => {
//     connectionSignalR.start().then(() => {
//       clearInterval(timer)
//       ElMessage.success("服务器websocket连接已恢复")
//     })
//   }, timeLength)
// })
/**重连时触发 */
connectionSignalR.onreconnecting((error) => {
  console.log("%c [ error ]-28", "font-size:13px; background:pink; color:#bf2c9f;", error)
  ElMessage.warning("服务器websocket连接关闭,尝试重连...")
})
/**重连成功触发 */
connectionSignalR.onreconnected((connectionId) => {
  console.log("%c [ connectionId ]-32", "font-size:13px; background:pink; color:#bf2c9f;", connectionId)
  ElMessage.success("服务器websocket连接已恢复")
})
/**singnalr */
const useSingnalR = {
  /** 开启连接*/
  startSignalR() {
    if (connectionSignalR.state !== singnalr.HubConnectionState.Disconnected) return
    console.log("进入了startSignalR")
    connectionSignalR
      .start()
      .then(() => {
        this.onReceiveSingnalR()
        ElMessage.success("连接成功")
      })
      .catch((err) => {
        ElMessage.error("连接失败")
        console.log(err)
      })
  },
  /** 断开连接*/
  stopSignalR() {
    if (connectionSignalR.state === singnalr.HubConnectionState.Disconnected) return
    connectionSignalR
      .stop()
      .then(() => {
        ElMessage.success("断开连接成功")
      })
      .catch((error) => {
        ElMessage.success("断开连接失败")
        console.log(error)
      })
  },
  /** 接收消息*/
  onReceiveSingnalR() {
    connectionSignalR.on("ReceiveMessageWeb", (message) => {
      var message = JSON.parse(message)
      console.log(`Received message from : ${message.WorkOrderId}`)
    })
  },
  /** 发送消息*/
  sendMessage(user: string, message: any) {
    console.log("%c [ user ]-73", "font-size:13px; background:pink; color:#bf2c9f;", user)
    console.log("%c [ message ]-41", "font-size:13px; background:pink; color:#bf2c9f;", message)
    connectionSignalR
      .invoke("SendMessage", user, message)
      .then(() => {
        ElMessage.success("发送成功")
      })
      .catch((error) => {
        console.log("%c [ error ]-80", "font-size:13px; background:pink; color:#bf2c9f;", error)
        ElMessage.error("发送失败")
      })
  }
}

export default useSingnalR
