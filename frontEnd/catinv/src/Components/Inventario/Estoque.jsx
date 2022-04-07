import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { toast } from 'react-toastify'
import api from './api'
import catapi from '../Catalogo/api'


function Add() {

  const [qty, setQty] = useState();
  const [item, setItem] = useState('Selecione o item');
  const [cat, setCat] = useState([])   

  const dados = {
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "catalogItemId": item,
    "quantity": qty
  }
  
  const irPara = useNavigate()

  useEffect(() => {
       
    catapi.get('').then(res => setCat(res.data))
   .catch(e => console.log('erro ', e))   
    
  }, [])  

  const valida = (e) => {

    e.preventDefault();
    if(item === 'Selecione o item'){
      toast.warning("Item não selecionado")
      return false
    } 
    
    const post = api.post('',dados).then(res => console.log('Resultado: ', res))
       .catch(e => console.log('Erro o Inserir ', e))
    
       if(post) toast.success("Produto adicionado com sucesso!")    

    irPara('/inventario')
  }


  return (
    <div className='container'>
      <div className="row">

        <h2 className="text-center mt-5">Armazenar</h2>

        <div className="col-md-6 shadow text-center mx-auto p-5">
          <form onSubmit={valida}>
            <div className="form-group mb-2">
              <select className="form-select" onChange={(e) => setItem(e.target.value)}>
                <option value="Selecione o item" selected>Selecione o item</option>
                {
                  cat.map(c => (
                    <option value={c.id}>{c.name}</option>
                  ))
                }
              </select>
            </div>

            <div className="form-group mb-2">
              <input type="number" value={qty}
                placeholder='Quantidade' className='form-control' onChange={e => setQty(e.target.value)} />
            </div>

            <div className="d-grid form-group mb-2">
              <button className="btn btn-dark" type="submit">Salvar</button>
            </div>

          </form>
        </div>
      </div>
    </div>
  )
}

export default Add