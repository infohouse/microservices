import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import api from './api';


function Add() {

  const [produto, setProduto] = useState("");
  const [preco, setPreco] = useState("");
  const [descricao, setDescricao] = useState("");

  const irPara = useNavigate()

  const dados = {
   'name': produto,
   'price' : preco,
   'description' : descricao
  }

  const valida = (e) =>{

    e.preventDefault();
    if(!produto || !preco || !descricao){
      toast.warning("Preencha todos os campos");
    }
    
    const post = api.post('',dados).then(res => console.log('Resultado: ', res))
      .catch(e => console.log('Erro o Inserir ', e))
    if(post) toast.success("Produto adicionado com sucesso!")    

    irPara('/')
  }

  
  return (
    <div className='container'>
      <div className="row">
        
      <h2 className="text-center mt-5">Novo Catálogo</h2>

        <div className="col-md-6 shadow text-center mx-auto p-5">
          <form onSubmit={valida}>
            <div className="form-group mb-2">
              <input type="text" value={produto} placeholder='Produto' 
                className='form-control' onChange={e => setProduto(e.target.value)}/>
            </div>

            <div className="form-group mb-2">
              <input type="text" value={descricao} placeholder='Descrição' 
              className='form-control' onChange={e => setDescricao(e.target.value)}/>
            </div>

            <div className="form-group mb-2">
              <input type="number" step="0.01" value={preco} 
              placeholder='Preço' className='form-control'  onChange={e => setPreco(e.target.value)}/>
            </div>

            <div className="d-grid form-group mb-2">
              <button className="btn btn-dark" type="submit">Adicionar</button>
            </div>            
              
          </form>
        </div>
      </div>      
    </div>
  )
}

export default Add