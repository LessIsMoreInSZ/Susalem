import { createApp } from 'vue';
import './plugins/assets';
// import axios from 'axios';
import { setupDayjs, setupIconifyOffline, setupLoading, setupNProgress } from './plugins';
import { setupStore } from './store';
import { setupRouter } from './router';
import { setupI18n } from './locales';
import App from './App.vue';

// import { getSysRolePage } from './service/api';

// getSysRolePage()
//   .then(res => {
//     console.log(res);
//   })
//   .catch(error => {
//     console.log(error);
//   });

async function setupApp() {
  setupLoading();

  setupNProgress();

  setupIconifyOffline();

  setupDayjs();

  const app = createApp(App);

  setupStore(app);

  await setupRouter(app);

  setupI18n(app);

  app.mount('#app');
}

setupApp();
