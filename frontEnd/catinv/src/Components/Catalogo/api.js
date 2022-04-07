import axios from 'axios'

export default  axios.create({
    baseURL: 'http://seu_ip_aki:8080/items', 
    headers: {
        "Content-type": "application/json"
      }
})