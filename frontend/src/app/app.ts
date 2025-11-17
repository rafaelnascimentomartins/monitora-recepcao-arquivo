import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastComponent } from "./shared/components/alerts/toast/toast.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, RouterOutlet,
    ToastComponent
],
  template: `
  <app-toast/>
  <router-outlet></router-outlet>
  `
})
export class App {
  protected readonly title = signal('mra.frontend');
}
