import React from 'react'
import '../style/style.css'
import { Link } from 'react-router-dom'
const SideMenu = () => {
    return (
        <div className='side-menu'>
            <Link className='side-menu-element' to="/showlist">Predstave</Link>
            <Link className='side-menu-element' to="/piecealllist">Pozorisni komadi</Link>
        </div>
    )
}

export default SideMenu
