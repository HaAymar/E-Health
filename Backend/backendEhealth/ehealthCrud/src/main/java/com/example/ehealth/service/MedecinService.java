package com.example.ehealth.service;

import com.example.ehealth.entity.Medecin;
import com.example.ehealth.repository.MedecinRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class MedecinService {

    @Autowired
    private MedecinRepository medecinRepository;

    public List<Medecin> getAllMedecins() {
        return medecinRepository.findAll();
    }

    public Medecin getMedecinById(Long id) {
        return medecinRepository.findById(id).orElse(null);
    }

    public Medecin createOrUpdateMedecin(Medecin medecin) {
        return medecinRepository.save(medecin);
    }

    public void deleteMedecin(Long id) {
        medecinRepository.deleteById(id);
    }
}