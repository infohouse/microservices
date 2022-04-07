import axios from 'axios'

export default  axios.create({
    baseURL: 'http://seu_ip_aki:8081/items',
    headers: {
        "Content-type": "application/json"
      }
})