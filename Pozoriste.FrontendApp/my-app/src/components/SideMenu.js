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
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/piecealllist">Pozorisni komadi</Link>
                
            }
            <span class="divider"></span>
            <Link className='side-menu-element' to="/pieceactivelist">Aktivni pozorisni komadi</Link>
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addPiece">Dodaj komad</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addactor">Dodaj glumca</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/actorlist">Svi glumci</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addshow">Dodaj predstavu</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addtheatre">Dodaj pozoriste</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showalltheatres">Pozorista</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showallAuditoriums">Sale</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addAuditorium">Dodaj salu</Link>
            }
        </div>
    )
}

export default SideMenu
