import React, { useEffect, useState }  from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { toast } from 'react-toastify';
import api from './api'

function Edit() {
  
  const {id} = useParams()
  const [produto, setProduto] = useState("");
  const [preco, setPreco] = useState("");
  const [descricao, setDescricao] = useState("");
  const irPara = useNavigate();

  const [dados, setDados] = useState([]) 
    

  useEffect(() => {
    api.get(id).then(res => (
    
      setDados(res.data)  
    
    )).catch(e => console.log('Erru ',e))

    
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])


  useEffect(() => {

    setDescricao(dados.description)
    setPreco(dados.price)
    setProduto(dados.name)

  },[dados])


  const update = {
    'name': produto,
    'price' : preco,
    'description' : descricao
   }

  const valida = (e) =>{

    e.preventDefault();
    
    api.put(id, update).then(res => console.log('atualizado ', res))
     .catch(e => console.log('Erru atualizar ', e))

    toast.success("Produto atualizado com sucesso!")
    irPara("/")
  }

  

  return (
    <div className='container'>
      <div className="row">
        
      <h2 className="text-center mt-5">Editar Catálogo</h2>

        <div className="col-md-6 shadow text-center mx-auto p-5">
          <form onSubmit={valida}>
            <div className="form-group mb-2">
              <input type="text" placeholder='Produto' value={produto} 
              onChange={e => setProduto(e.target.value)}
              className='form-control'/>
            </div>

            <div className="form-group mb-2">
              <input type="text" placeholder='Descrição'
               value={descricao} className='form-control'
               onChange={e => setDescricao(e.target.value)}
               />
            </div>

            <div className="form-group mb-2">
              <input type="number" step="0.01" placeholder='Preço'  
              value={preco} className='form-control'
              onChange={e => setPreco(e.target.value)}
              />
            </div>

            <div className="form-group mb-2">
              
              <button className="btn btn-dark me-2" type="submit">Atualizar</button>
              <Link to="/" className="btn btn-danger me-2" type="button">Cancelar</Link>
              
            </div>            
              
          </form>
        </div>
      </div>      
    </div>
    
  )
}

export default Edit