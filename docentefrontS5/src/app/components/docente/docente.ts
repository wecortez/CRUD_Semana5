import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DocenteService } from '../../services/docente.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-docente',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './docente.html',
  styleUrl: './docente.css',
})
export class Docente implements OnInit {
  listaDocentes = signal<any[]>([]);

  constructor(private readonly docenteService: DocenteService) {}

  ngOnInit(): void {
    this.cargarLista();
  }

  cargarLista() {
    this.docenteService.todos().subscribe((lista) => {
      this.listaDocentes.set(Array.isArray(lista) ? lista : []);
    });
  }

  eliminar(id: number) {
    Swal.fire({
      title: '¿Desea eliminar el registro?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
      confirmButtonColor: '#d33',
      cancelButtonColor: '#6c757d',
    }).then((result) => {
      if (result.isConfirmed) {
        this.docenteService.eliminar(id).subscribe((response) => {
          if (response == null) {
            Swal.fire('Eliminado', 'El docente fue eliminado con éxito', 'success');
            this.cargarLista();
          }
        });
      }
    });
  }
}
