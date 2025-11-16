import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, CommonModule
  ],
  template: `<router-outlet></router-outlet>`
})
export class App {
  protected readonly title = signal('mra.frontend');
}
