import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { toast } from 'react-toastify';
import api from './api'

function Home() {
  
  const [dados, setDados] = useState([])
  
  useEffect( () => {

    api.get()
    .then(res => setDados(res.data))

  }, [dados])

  const removeCat = (id) => {
    
    api.delete(id).then(res => toast.success("Catalogo removido com Sucesso!"))
     .catch(e => toast.error('Erru deletar ',e))
    
  } 
  

  return (
    <div className='container'>
      <div className="row">
        <div className="col-md-12 my-2 text-end">
          <Link to="/add" className="btn btn-outline-dark">Add</Link>
        </div>
        <div className="col-md-12 text-start">
          <h2 className='text-center text-warning fw-bold'>Catálogo</h2>
          <table className="table table-hover mt-3">
           <thead className="text-white bg-dark text-center">
             <tr className='text-center'>
               <th>Produto</th>
               <th>Descrição</th>
               <th>Preço</th>
               <th>Ação</th>
             </tr>
           </thead>
           <tbody>

                 {
                   dados.map(c => {
                     return(
                      <tr key={c.id} className='text-center'>
                        <td>{c.name}</td>
                        <td>{c.description}</td>
                        <td>{c.price}</td>
                        <td className='text-center'>
                        <Link to={`/edit/${c.id}`} className='btn btn-outline-info me-2'>Editar</Link>
                        <button type='button'
                        onClick={() => removeCat(c.id)}
                        className='btn btn-outline-danger'>Excluir</button>
                        </td>
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
export default Home