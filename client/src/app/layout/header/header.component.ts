import { Component, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import{MatIconModule} from '@angular/material/icon';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { BusyService } from '../../core/services/busy.service';
import { MatProgressBar } from '@angular/material/progress-bar';

@Component({
  selector: 'app-header',
  imports: [MatIconModule, MatButton, MatBadge, RouterLink,
    RouterLinkActive,MatProgressBar],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  busyService = inject(BusyService);
}
