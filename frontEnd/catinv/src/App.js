import { Route, Routes } from 'react-router-dom';
import Home from './Components/Catalogo/home'
import Add from './Components/Catalogo/Add'
import Edit from './Components/Catalogo/Edit'
import Navbar from './Components/Navbar'
import Inventario from './Components/Inventario/inventario'
import Estoque from './Components/Inventario/Estoque'
import { ToastContainer } from 'react-toastify';

function App() {
  return (
    <div className="App">
      
      <Navbar />
      <ToastContainer />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/home' element={<Home />} />
        <Route path='/add' element={<Add />} />
        <Route path='/edit/:id' element={<Edit />} />
        <Route path='/inventario' element={<Inventario />} />
        <Route path='/estoque' element={<Estoque />} />
      </Routes>
         
    </div>
  );
}

export default App;