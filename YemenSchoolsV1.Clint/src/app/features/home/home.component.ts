import { Component } from '@angular/core';
import { HeroSectionComponent } from "./hero-section/hero-section.component";
import { AboutSectionComponent } from "./about-section/about-section.component";
import { HowWeWorkSectionComponent } from "./how-we-work-section/how-we-work-section.component";
import { ServicesSectionComponent } from "./services-section/services-section.component";
import { ServicesAltSectionComponent } from "./services-alt-section/services-alt-section.component";
import { CallToActionSectionComponent } from "./call-to-action-section/call-to-action-section.component";
import { PricingSectionComponent } from "./pricing-section/pricing-section.component";
import { FaqSectionComponent } from "./faq-section/faq-section.component";
import { TestimonialsSectionComponent } from "./testimonials-section/testimonials-section.component";
import { ContactSectionComponent } from "./contact-section/contact-section.component";
import { SchoolItemComponent } from "../schools/school-item/school-item.component";
import { TitelSectionComponent } from "../../shared/components/titel-section/titel-section.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroSectionComponent, AboutSectionComponent, HowWeWorkSectionComponent, ServicesSectionComponent, ServicesAltSectionComponent, CallToActionSectionComponent, PricingSectionComponent, FaqSectionComponent, TestimonialsSectionComponent, ContactSectionComponent, SchoolItemComponent, TitelSectionComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
