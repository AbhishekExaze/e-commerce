import { Component } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import{MatIconModule} from '@angular/material/icon';
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-header',
  imports: [MatIconModule, MatButton, MatBadge, RouterLink,RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {}
