import { createApp } from 'vue'
import App from './App.vue'
import router from './router'


import './assets/main.css'

const app = createApp(App)

app.config.globalProperties.API_URL = 'http://localhost:1889/api'

app.use(router)

app.mount('#app')
