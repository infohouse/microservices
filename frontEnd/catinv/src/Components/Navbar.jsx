import React from 'react'
import { Link } from 'react-router-dom'

const Navegacao = () => {
    return (


        <nav className="navbar navbar-dark bg-dark py-2 navbar-expand-lg">
            <Link to="/" className="navbar-brand ms-5" id='brand'>microServices</Link>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarSupportedContent">
                <ul className="navbar-nav ms-auto mb-2 mb-lg-0"  id='ft'>
                    <li className="nav-item">
                        <Link to='/inventario' className='nav-link active'>Inventário</Link>
                    </li>

                    <li className="nav-item me-5">
                        <Link to='/' className='nav-link active'>Catálogo</Link>
                    </li>
                </ul>

            </div>
        </nav>

    )
}

export default Navegacao