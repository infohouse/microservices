import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import api from './api'

function inventario() {

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const [dados, setDados] = useState([])
  

  // eslint-disable-next-line react-hooks/rules-of-hooks
  useEffect(() => {
                   
    api.get('?userId=3fa85f64-5717-4562-b3fc-2c963f66afa6').then(res => setDados(res.data))
   .catch(e => console.log('erro ', e))   
    
  }, [dados])  

  return (
    
    <div className='container'>
    <div className="row">
      <div className="col-md-12 my-2 text-end">
      <Link to="/estoque" className="btn btn-outline-dark">Armazenar</Link>
      </div>
      <div className="col-md-12 text-start">
        <h2 className='text-center text-warning fw-bold'>Inventário</h2>
        <table className="table table-hover mt-3">
         <thead className="text-white bg-dark text-center">
           <tr>
             <th>Produto</th>
             <th>Descrição</th>
             <th>Estoque</th>             
           </tr>
         </thead>
         <tbody>
         {
        dados.map(i => {
          return(
            <tr key={i.catalogItemId} className="text-center">
              <td>{i.name}</td>
              <td>{i.description}</td>
              <td>{i.quantity}</td>
            </tr>
          )
        })
      }
         </tbody>
          </table>
        </div>
      </div>      
    </div>
  )
}

export default inventario