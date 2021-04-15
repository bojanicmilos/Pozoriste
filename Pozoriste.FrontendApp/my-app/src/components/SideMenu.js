import React from 'react'
import '../style/style.css'
import { Link } from 'react-router-dom'
import { getRole } from './globalStorage/RoleCheck'
import { isUserLogged } from './globalStorage/IsUserLogged'
import AddTheatre from './Admin/TheatreActions/AddTheatre'

const SideMenu = () => {

    return (
        <div className='side-menu'>
            <Link className='side-menu-element' to="/showlist">Predstave</Link>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/piecealllist">Pozorisni komadi</Link>
            }
            <Link className='side-menu-element' to="/pieceactivelist">Aktivni pozorisni komadi</Link>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addPiece">Dodaj komad</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addactor">Dodaj glumca</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/actorlist">Svi glumci</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addshow">Dodaj predstavu</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addtheatre">Dodaj pozoriste</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showalltheatres">Pozorista</Link>
            }
        </div>
    )
}

export default SideMenu
