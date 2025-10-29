package com.example.demo.repogitory;

import com.example.demo.entity.ArrowData;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ArrowDataRepo extends JpaRepository<ArrowData,Long> {

}
